using FinantialManager.Domain.Models;
using FinantialManager.Infra.CrossCutting.Util;
using Xunit;

namespace FinantialManager.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestReadXML()
        {
            var input = OFXUtils.ReadOFXtoXMLFromPath(@"D:\Rubens\Desktop\DevelopersChallenge2-master\DevelopersChallenge2-master\OFX\extrato1.ofx");
            var ofx = OFX.GetOFXFromString(input);
        }
    }
}
