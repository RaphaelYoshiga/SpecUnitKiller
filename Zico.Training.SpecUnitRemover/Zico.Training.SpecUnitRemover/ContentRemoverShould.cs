using FluentAssertions;
using NUnit.Framework;
using Zico.Training.SpecUnitRemover.App;

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

        [TestCase(" .And(i_send_a_heartbeat_request)        .And(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request();    i_send_a_heartbeat_request();")]
        [TestCase(" .When(i_send_a_heartbeat_request)       .When(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request();   i_send_a_heartbeat_request();")]
        [TestCase(" .Then(i_send_a_heartbeat_request)       .Then(i_send_a_heartbeat_request)", " i_send_a_heartbeat_request();   i_send_a_heartbeat_request();")]
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

        [Test]
        public void SimpleRemoveSpecUnitKeepSpacing()
        {
            var result = _contentRemover.Remove(@"            Given(i_have_an_instance_of_matflo)
                .When(i_send_a_heartbeat_request)
                .And(i_send_a_heartbeat_request)
                .Then(we_only_call_matfloApi_0_, 1, ""heartbeat"");");

            result.Should().Be(@"            i_have_an_instance_of_matflo();
            i_send_a_heartbeat_request();
            i_send_a_heartbeat_request();
            we_only_call_matfloApi_0_( 1, ""heartbeat"");");
        }

        [TestCase(" Given(we_only_call_matfloApi_0_, 1, Heartbeat.ToString())", " we_only_call_matfloApi_0_( 1, Heartbeat.ToString());")]
        [TestCase(" Given(we_only_call_matfloApi_0_, Heartbeat.ToString(), Heartbeat.ToString())", " we_only_call_matfloApi_0_( Heartbeat.ToString(), Heartbeat.ToString());")]
        public void NotChangeBracketsOnMethodsInParameters(string parse, string expected)
        {
            var result = _contentRemover.Remove(parse);
            result.Should().Be(expected);
        }

        [Test]
        public void FixIndentationAfterSpecUnitRemoval()
        {
            var result = _contentRemover.Remove(@"        [Test]
        public void Collaborate_with_matflo_api_to_send_heartbeat_only_when_no_previous_heartbeat_sent()
        {
            Given(i_have_an_instance_of_matflo)
                .When(i_send_a_heartbeat_request)
                .And(i_send_a_heartbeat_request)
                .Then(we_only_call_matfloApi_0_, 1, heartbeat.ToString());
        }");

            result.Should().Be(@"        [Test]
        public void Collaborate_with_matflo_api_to_send_heartbeat_only_when_no_previous_heartbeat_sent()
        {
            i_have_an_instance_of_matflo();
            i_send_a_heartbeat_request();
            i_send_a_heartbeat_request();
            we_only_call_matfloApi_0_( 1, heartbeat.ToString());
        }");
        }
    }
}