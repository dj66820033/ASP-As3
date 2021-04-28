using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        Database database = new Database();

        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                List<Question> questions = database.Questions.ToList();
                List<Answer> answers = database.Answers.ToList();
                List<Category> categories = database.Categories.ToList();

                List<Question> ques = questions.GetRange(0, questions.Count());
                var QnA = (from q in questions
                           join a in answers
                           on q.QuestionID equals a.QuestionID

                           select new Question
                           {
                               QuestionID = q.QuestionID,
                               QuestionName = q.QuestionName,
                               QuestionTime=q.QuestionTime,
                               vote=q.vote,
                               viewnum=q.viewnum,
                               CategoryID=q.CategoryID
                           }).ToList();

                Dictionary<int, int> ans_ed = new Dictionary<int, int>();
                foreach(var item in QnA.Select(id => id.QuestionID).Distinct().ToList())
                {
                    var answer = (from a in answers
                                  where a.QuestionID.Equals(item)
                                  select a.AnswerID);
                    ans_ed.Add(item, answer.Count());
                    ques.RemoveAll(id => id.QuestionID == item);
                }
                var q1 = (from q in questions
                          join a in ans_ed
                          on q.QuestionID equals a.Key
                          join c in categories
                          on q.CategoryID equals c.CategoryID
                          select new Question
                          {
                              QuestionID = q.QuestionID,
                              QuestionName = q.QuestionName,
                              QuestionTime = q.QuestionTime,
                              vote = q.vote,
                              viewnum = q.viewnum,
                              CategoryName = c.CategoryName,
                              UserName=q.UserName,
                              ans_ed = a.Value
                          }).ToList();
                var q2 = (from q in ques
                          join c in categories
                          on q.CategoryID equals c.CategoryID
                          select new Question
                          {
                              QuestionID = q.QuestionID,
                              QuestionName = q.QuestionName,
                              QuestionTime = q.QuestionTime,
                              vote = q.vote,
                              viewnum = q.viewnum,
                              CategoryName = c.CategoryName,
                              UserName = q.UserName,
                              ans_ed = 0
                          }).ToList();
                q1.AddRange(q2);
                return View(q1);
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View(database.Categories.ToList());
        }

        public ActionResult Question()
        {
            List<Question> questions = database.Questions.ToList();
            List<Answer> answers = database.Answers.ToList();
            List<Category> categories = database.Categories.ToList();

            List<Question> ques = questions.GetRange(0, questions.Count());
            var QnA = (from q in questions
                       join a in answers
                       on q.QuestionID equals a.QuestionID

                       select new Question
                       {
                           QuestionID = q.QuestionID,
                           QuestionName = q.QuestionName,
                           QuestionTime = q.QuestionTime,
                           vote = q.vote,
                           viewnum = q.viewnum,
                           CategoryID = q.CategoryID
                       }).ToList();

            Dictionary<int, int> ans_ed = new Dictionary<int, int>();
            foreach (var item in QnA.Select(id => id.QuestionID).Distinct().ToList())
            {
                var answer = (from a in answers
                              where a.QuestionID.Equals(item)
                              select a.AnswerID);
                ans_ed.Add(item, answer.Count());
                ques.RemoveAll(id => id.QuestionID == item);
            }
            var q1 = (from q in questions
                      join a in ans_ed
                      on q.QuestionID equals a.Key
                      join c in categories
                      on q.CategoryID equals c.CategoryID
                      select new Question
                      {
                          QuestionID = q.QuestionID,
                          QuestionName = q.QuestionName,
                          QuestionTime = q.QuestionTime,
                          vote = q.vote,
                          viewnum = q.viewnum,
                          CategoryName = c.CategoryName,
                          UserName = q.UserName,
                          ans_ed = a.Value
                      }).ToList();
            var q2 = (from q in ques
                      join c in categories
                      on q.CategoryID equals c.CategoryID
                      select new Question
                      {
                          QuestionID = q.QuestionID,
                          QuestionName = q.QuestionName,
                          QuestionTime = q.QuestionTime,
                          vote = q.vote,
                          viewnum = q.viewnum,
                          CategoryName = c.CategoryName,
                          UserName = q.UserName,
                          ans_ed = 0
                      }).ToList();
            q1.AddRange(q2);
            return View(q1);
        }

        public ActionResult Login()
        {
            if (Session["UserName"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var username = database.Users.Where(u => u.UserName.Equals(user.UserName));
                if (username.Count() > 0)
                {
                    var password = username.Where(p => p.password.Equals(user.password));
                    if (password.Count() > 0)
                    {
                        Session["UserName"] = user.UserName;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Password error";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "The user name does not exist";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user,string psw)
        {
            if (ModelState.IsValid)
            {
                var checkname = database.Users.Where(u => u.UserName.Equals(user.UserName));
                if (checkname.Count() > 0)
                {
                    ViewBag.error = "The user name exists";
                    return View();
                }
                else
                {
                    if (user.password == psw)
                    {
                        database.Users.Add(user);
                        database.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.error = "Entered passwords differ";
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }
        }
    }
}