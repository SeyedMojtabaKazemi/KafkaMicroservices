using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProducerMicroservice.Model;

namespace ProducerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private ProducerConfig _config;
        public ProducerController(ProducerConfig config)
        {
            _config = config;
        }

        [HttpPost("SendMessage")]
        public async Task<ActionResult> SendMessage(string topic, [FromBody] Person person)
        {
            string serializedPerson = JsonConvert.SerializeObject(person);
            using (var producer = new ProducerBuilder<string, string>(_config).Build())
            {
                // TopicPartition partition = new TopicPartition(topic, new Partition(2));
                await producer.ProduceAsync(topic, new Message<string, string> {Key = Guid.NewGuid().ToString(), Value = serializedPerson });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }
    }
}