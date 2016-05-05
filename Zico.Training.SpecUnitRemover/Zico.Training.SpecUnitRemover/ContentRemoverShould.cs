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
        [TestCase(" .Then(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request();")]
        public void SimpleSpecUnitRemover(string parse, string expected)
        {
            ContentRemover remover = new ContentRemover();

            var result = remover.Remove(parse);

            result.Should().Be(expected);
        }

        [TestCase(" .And(i_send_a_heartbeat_request) .And(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request(); i_send_a_heartbeat_request();")]
        [TestCase(" .When(i_send_a_heartbeat_request) .When(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request(); i_send_a_heartbeat_request();")]
        [TestCase(" .Then(i_send_a_heartbeat_request) .Then(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request(); i_send_a_heartbeat_request();")]
        [TestCase(" Given(i_send_a_heartbeat_request) Given(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request(); i_send_a_heartbeat_request();")]
        public void LoopForMultipleClauses(string parse, string expected)
        {
            ContentRemover remover = new ContentRemover();

            var result = remover.Remove(parse);

            result.Should().Be(expected);
        }
    }
}