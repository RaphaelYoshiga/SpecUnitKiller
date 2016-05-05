using FluentAssertions;
using NUnit.Framework;

namespace Zico.Training.SpecUnitRemover
{
    [TestFixture]
    public class ContentRemoverShould
    {
        [TestCase(" Given(i_have_an_instance_of_matflo)", " i_have_an_instance_of_matflo();")]
        [TestCase(" .When(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request();")]
        [TestCase(" .And(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request();")]
        public void ReplaceGiven(string parse, string expected)
        {
            ContentRemover remover = new ContentRemover();

            var result = remover.Remove(parse);

            result.Should().Be(expected);
        }

        private const string intialString = @"
            Given(i_have_an_instance_of_matflo)
                .When(i_send_a_heartbeat_request)
                .And(i_send_a_heartbeat_request)
                .Then(we_only_call_matfloApi_0_, 1, ""heartbeat"");";
    }
}