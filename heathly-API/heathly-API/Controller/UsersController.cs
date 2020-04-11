using heathly_API.Model;
using System;
using System.Linq;
using System.Text;
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
                        IsLoggedIn = true,
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
                    IsLoggedIn = false,
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
                var result = db.Users.Where(x => x.username == user.userName ).Select(y => new { UserFound = true }).FirstOrDefault();
                if (result != null)
                {
                    return Ok(new { Status = false, Message = "User already exists" });
                }
                else
                {
                    string _token = generateToken();
                    var _user = new Users { username = user.userName, password = user.password, token = _token };
                    db.Users.Add(_user);
                    db.SaveChanges();
                    return Ok(new { Status = true, Message = "User created" });
                }
            }
            else return BadRequest();

        }

        public string generateToken() {
            int longitud = 7;
            const string alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder token = new StringBuilder();
            Random rnd = new Random();

            for (int i = 0; i < longitud; i++)
            {
                int indice = rnd.Next(alfabeto.Length);
                token.Append(alfabeto[indice]);
            }

            return token.ToString();
        }
    }
}
