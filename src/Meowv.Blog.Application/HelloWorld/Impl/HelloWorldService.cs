namespace Meowv.Blog.HelloWorld.Impl
{
    public class HelloWorldService:MeowvBlogApplicationServiceBase,IHelloWorldService
    {
        public string HelloWorld()
        {
            return "Hello World";
            //
        }
    }
}