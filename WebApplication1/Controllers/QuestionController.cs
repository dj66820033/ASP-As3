using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class QuestionController : Controller
    {
        Database database = new Database();
        // GET: Question
        public ActionResult GetQuestion(int id)
        {
            List<Question> questions = database.Questions.ToList();
            List<Answer> answers = database.Answers.ToList();
            List<Category> categories = database.Categories.ToList();

            var view = database.Questions.FirstOrDefault(qid => qid.QuestionID == id);
            view.viewnum = view.viewnum + 1;
            database.SaveChanges();

            var answer = (from a in answers
                          where a.QuestionID.Equals(id)
                          select new Answer
                          {
                              AnswerID = a.AnswerID,
                              AnswerText = a.AnswerText,
                              AnswerTime = a.AnswerTime,
                              QuestionID = a.QuestionID,
                              username = a.username
                          }).ToList();
            var question = (from q in questions
                            join c in categories
                            on q.CategoryID equals c.CategoryID
                            where q.QuestionID.Equals(id)
                            select new Question
                            {
                                QuestionID = q.QuestionID,
                                QuestionName = q.QuestionName,
                                QuestionTime = q.QuestionTime,
                                vote = q.vote,
                                viewnum = q.viewnum,
                                CategoryName = c.CategoryName,
                                UserName = q.UserName,
                                answers = answer
                            }).ToList();
            return View(question);
        }

        [HttpPost]
        public ActionResult AddAnswer(Question question)
        {
            Answer answer = new Answer();
            answer.AnswerText = question.answer.AnswerText;
            answer.AnswerTime = DateTime.Now;
            answer.QuestionID = question.QuestionID;
            answer.username = Session["UserName"].ToString();
            database.Answers.Add(answer);
            database.SaveChanges();
            return RedirectToAction("GetQuestion", "Question", new { id = question.QuestionID });
        }
    }
}