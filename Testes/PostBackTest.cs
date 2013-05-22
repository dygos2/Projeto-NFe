using Microsoft.VisualStudio.TestTools.UnitTesting;
using FN4IntegracaoPostBackCtl;

namespace Testes
{
    [TestClass]
    public class PostBackTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var poster = new PostSubmitter("http://208.109.236.57:8012/recebe_post.asp", 45000)
                             {Type = PostSubmitter.PostTypeEnum.Post};

            poster.PostItems.Add("campo1", "dado1§dado2§dado3");
            poster.PostItems.Add("campo2", "dado4§dado5§dado6");
            poster.PostItems.Add("campo3", "dado7§dado8§dado9");

            var resultado = poster.Post();
            
            Assert.IsFalse(string.IsNullOrWhiteSpace(resultado));
        }
    }
}
