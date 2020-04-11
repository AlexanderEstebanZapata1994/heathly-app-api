using heathly_API.Model;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace heathly_API.Controller
{
    [RoutePrefix("api/users")]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public IHttpActionResult GetUser([FromBody] UserRequest user)
        {
            Entities db = new Entities();
            UserResponse User = db.Users.Where(x => x.password == user.password && x.username == user.userName).Select(
                    userFound => new UserResponse  { 
                        UserId = userFound.Id,
                        UserName = userFound.username,
                        Token = userFound.token ,
                        IsLoggedIn = false,
                        Error = new ErrorType  { 
                            HasError = false,        
                            ErrorMessage = "",
                        }
                    }).FirstOrDefault();

            if (User == null){
                User = new UserResponse
                {
                    UserId = 0,
                    UserName = "",
                    Token = "",
                    IsLoggedIn = true,
                    Error = new ErrorType
                    {
                        HasError = true,
                        ErrorMessage = "User does not exists",
                    }
                };
                return Ok(User);
            }
            else
                return Ok(User);
        }

        [HttpPost]
        [Route("register")]
        public IHttpActionResult RegisterUser([FromBody] UserRequest user) {
            Entities db = new Entities();

            if (user != null)
            {
                var _user = new Users { username = user.userName, password = user.password, token = "" };
                var result = db.Users.Where(x => x.username == _user.username && x.password == _user.password).Select(y => new { UserFound = true }).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new { Status = false, Message = "User already exists" });
                }
                else
                {
                    db.Users.Add(_user);
                    db.SaveChanges();
                    return Ok(new { Status = true, Message = "User created" });
                }
            }
            else return BadRequest();

        }
    }
}
