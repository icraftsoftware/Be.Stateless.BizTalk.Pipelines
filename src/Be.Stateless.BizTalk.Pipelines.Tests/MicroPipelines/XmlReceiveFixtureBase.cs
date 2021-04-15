#region Copyright & License

// Copyright © 2012 - 2021 François Chabot
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.IO;
using Be.Stateless.BizTalk.Component;
using Be.Stateless.BizTalk.ContextProperties.Subscribable;
using Be.Stateless.BizTalk.Message.Extensions;
using Be.Stateless.BizTalk.MicroComponent;
using Be.Stateless.BizTalk.Schema.Annotation;
using Be.Stateless.IO;
using FluentAssertions;
using Microsoft.XLANGs.BaseTypes;
using Winterdom.BizTalk.PipelineTesting;

namespace Be.Stateless.BizTalk.MicroPipelines
{
	public abstract class XmlReceiveFixtureBase
	{
		protected void ContextPropertyExtractorClearsPromotedProperty(ReceivePipelineWrapper pipeline)
		{
			const string content = "<ns0:Root xmlns:ns0=\"http://schemas.microsoft.com/BizTalk/2003/Any\"><message>content</message></ns0:Root>";
			using (var stream = new StringStream(content))
			{
				pipeline.AddDocSpec(typeof(Any));
				var microPipeline = (MicroPipelineComponent) pipeline.GetComponent(PipelineStage.Decode, 1);
				microPipeline.Components = new[] {
					new ContextPropertyExtractor { Extractors = new[] { new PropertyExtractor(BizTalkFactoryProperties.CorrelationId.QName, ExtractionMode.Clear) } }
				};

				var inputMessage = MessageHelper.CreateFromStream(stream);
				inputMessage.Promote(BizTalkFactoryProperties.CorrelationId, "promoted-token");
				inputMessage.GetProperty(BizTalkFactoryProperties.CorrelationId).Should().Be("promoted-token");
				inputMessage.IsPromoted(BizTalkFactoryProperties.CorrelationId).Should().BeTrue();

				var outputMessages = pipeline.Execute(inputMessage);

				outputMessages[0].GetProperty(BizTalkFactoryProperties.CorrelationId).Should().BeNull();
				outputMessages[0].IsPromoted(BizTalkFactoryProperties.CorrelationId).Should().BeFalse();
				using (var reader = new StreamReader(outputMessages[0].BodyPart.Data))
				{
					var readOuterXml = reader.ReadToEnd();
					readOuterXml.Should().Be(content);
				}
			}
		}

		public void ContextPropertyExtractorClearsWrittenProperty(ReceivePipelineWrapper pipeline)
		{
			const string content = "<ns0:Root xmlns:ns0=\"http://schemas.microsoft.com/BizTalk/2003/Any\"><message>content</message></ns0:Root>";
			using (var stream = new StringStream(content))
			{
				pipeline.AddDocSpec(typeof(Any));
				var microPipeline = (MicroPipelineComponent) pipeline.GetComponent(PipelineStage.Decode, 1);
				microPipeline.Components = new[] {
					new ContextPropertyExtractor { Extractors = new[] { new PropertyExtractor(BizTalkFactoryProperties.CorrelationId.QName, ExtractionMode.Clear) } }
				};

				var inputMessage = MessageHelper.CreateFromStream(stream);
				inputMessage.SetProperty(BizTalkFactoryProperties.CorrelationId, "written-token");
				inputMessage.GetProperty(BizTalkFactoryProperties.CorrelationId).Should().Be("written-token");
				inputMessage.IsPromoted(BizTalkFactoryProperties.CorrelationId).Should().BeFalse();

				var outputMessages = pipeline.Execute(inputMessage);

				outputMessages[0].GetProperty(BizTalkFactoryProperties.CorrelationId).Should().BeNull();
				outputMessages[0].IsPromoted(BizTalkFactoryProperties.CorrelationId).Should().BeFalse();
				using (var reader = new StreamReader(outputMessages[0].BodyPart.Data))
				{
					var readOuterXml = reader.ReadToEnd();
					readOuterXml.Should().Be(content);
				}
			}
		}

		public void ContextPropertyExtractorPromotesConstant(ReceivePipelineWrapper pipeline)
		{
			const string content = "<ns0:Root xmlns:ns0=\"http://schemas.microsoft.com/BizTalk/2003/Any\"><message>content</message></ns0:Root>";
			using (var stream = new StringStream(content))
			{
				pipeline.AddDocSpec(typeof(Any));
				var microPipeline = (MicroPipelineComponent) pipeline.GetComponent(PipelineStage.Decode, 1);
				microPipeline.Components = new[] {
					new ContextPropertyExtractor {
						Extractors = new[] {
							new ConstantExtractor(BizTalkFactoryProperties.EnvironmentTag.QName, "tag", ExtractionMode.Promote)
						}
					}
				};

				var inputMessage = MessageHelper.CreateFromStream(stream);
				inputMessage.GetProperty(BizTalkFactoryProperties.EnvironmentTag).Should().BeNull();
				inputMessage.IsPromoted(BizTalkFactoryProperties.EnvironmentTag).Should().BeFalse();

				var outputMessages = pipeline.Execute(inputMessage);

				outputMessages[0].GetProperty(BizTalkFactoryProperties.EnvironmentTag).Should().Be("tag");
				outputMessages[0].IsPromoted(BizTalkFactoryProperties.EnvironmentTag).Should().BeTrue();
				using (var reader = new StreamReader(outputMessages[0].BodyPart.Data))
				{
					var readOuterXml = reader.ReadToEnd();
					readOuterXml.Should().Be(content);
				}
				outputMessages[0].GetProperty(BizTalkFactoryProperties.EnvironmentTag).Should().Be("tag");
				outputMessages[0].IsPromoted(BizTalkFactoryProperties.EnvironmentTag).Should().BeTrue();
			}
		}
	}
}
