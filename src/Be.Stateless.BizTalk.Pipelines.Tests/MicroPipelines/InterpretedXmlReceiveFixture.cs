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

extern alias Interpreted;
using Be.Stateless.BizTalk.Dsl.Pipeline.Interpreters;
using Winterdom.BizTalk.PipelineTesting;
using Xunit;
using XmlReceive = Interpreted::Be.Stateless.BizTalk.MicroPipelines.XmlReceive;

namespace Be.Stateless.BizTalk.MicroPipelines
{
	public class InterpretedXmlReceiveFixture : XmlReceiveFixtureBase
	{
		[Fact]
		public void ContextPropertyExtractorClearsPromotedProperty()
		{
			ContextPropertyExtractorClearsPromotedProperty(PipelineFactory.CreateReceivePipeline(typeof(ReceivePipelineInterpreter<XmlReceive>)));
		}

		[Fact]
		public void ContextPropertyExtractorClearsWrittenProperty()
		{
			ContextPropertyExtractorClearsWrittenProperty(PipelineFactory.CreateReceivePipeline(typeof(ReceivePipelineInterpreter<XmlReceive>)));
		}

		[Fact]
		public void ContextPropertyExtractorPromotesConstant()
		{
			ContextPropertyExtractorPromotesConstant(PipelineFactory.CreateReceivePipeline(typeof(ReceivePipelineInterpreter<XmlReceive>)));
		}
	}
}
