using FluentAssertions;
using NUnit.Framework;

namespace Zico.Training.SpecUnitRemover
{
    [TestFixture]
    public class ContentRemoverShould
    {
        private string parse =@"[Test]
        public void Collaborate_with_matflo_api_to_send_heartbeat_only_when_no_previous_heartbeat_sent()
        {
            Given(i_have_an_instance_of_matflo)
                .When(i_send_a_heartbeat_request)
                .And(i_send_a_heartbeat_request)
                .Then(we_only_call_matfloApi_0_, 1, ""heartbeat"");
        }";

        private string expected = @"[Test]
        public void Collaborate_with_matflo_api_to_send_heartbeat_only_when_no_previous_heartbeat_sent()
        {
            i_have_an_instance_of_matflo();
                .When(i_send_a_heartbeat_request)
                .And(i_send_a_heartbeat_request)
                .Then(we_only_call_matfloApi_0_, 1, ""heartbeat"");
        }";

        [Test]
        public void ReplaceGiven()
        {
            ContentRemover remover = new ContentRemover();

            var result = remover.Remove(parse);

            result.Should().Be(expected);
        }
    }
}