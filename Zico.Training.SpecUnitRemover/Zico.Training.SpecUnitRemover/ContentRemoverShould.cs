using FluentAssertions;
using NUnit.Framework;

namespace Zico.Training.SpecUnitRemover
{
    [TestFixture]
    public class ContentRemoverShould
    {
        private ContentRemover _contentRemover;

        [SetUp]
        public void SetUp()
        {
            _contentRemover = new ContentRemover();
        }

        [TestCase(" Given(i_have_an_instance_of_matflo)", " i_have_an_instance_of_matflo();")]
        [TestCase(" .When(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request();")]
        [TestCase(" .And(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request();")]
        [TestCase(" .Then(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request();")]
        public void SimpleSpecUnitRemover(string parse, string expected)
        {
            var result = _contentRemover.Remove(parse);
            result.Should().Be(expected);
        }

        [TestCase(" .And(i_send_a_heartbeat_request) .And(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request(); i_send_a_heartbeat_request();")]
        [TestCase(" .When(i_send_a_heartbeat_request) .When(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request(); i_send_a_heartbeat_request();")]
        [TestCase(" .Then(i_send_a_heartbeat_request) .Then(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request(); i_send_a_heartbeat_request();")]
        [TestCase(" Given(i_send_a_heartbeat_request) Given(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request(); i_send_a_heartbeat_request();")]
        public void LoopForMultipleClauses(string parse, string expected)
        {
            var result = _contentRemover.Remove(parse);
            result.Should().Be(expected);
        }

        [TestCase(" .Then(we_only_call_matfloApi_0_, 1)", " we_only_call_matfloApi_0_( 1);")]
        [TestCase(" .When(we_only_call_matfloApi_0_, 1, \"Heartbeat\")", " we_only_call_matfloApi_0_( 1, \"Heartbeat\");")]
        [TestCase(" Given(we_only_call_matfloApi_0_, 1, \"Heartbeat\")", " we_only_call_matfloApi_0_( 1, \"Heartbeat\");")]
        public void RemoveSpecUnitForClausesWithParameters(string parse, string expected)
        {
            var result = _contentRemover.Remove(parse);
            result.Should().Be(expected);
        }
    }
}