using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace UnitTestsSamples.Sample2
{

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int Status { get; set; }
    }

    public interface IUserDao
    {
        User GetById(int id);
    }

    [SuppressMessage("ReSharper", "Mvc.ViewNotResolved")]
    [SuppressMessage("ReSharper", "Mvc.ActionNotResolved")]
    public class UserController : Controller
    {
        private readonly IUserDao _userDao;
        public UserController(IUserDao userDao)
        {
            _userDao = userDao;
        }

        [HttpGet]
        public ActionResult UserInfo(int id)
        {
            var user = _userDao.GetById(id);
            if (user == null || user.Status != 1)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}
