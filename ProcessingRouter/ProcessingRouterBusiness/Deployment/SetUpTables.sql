
GO
/****** Object:  Table [dbo].[processingRouter_ParameterSets]    Script Date: 28/10/2016 3:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[processingRouter_ParameterSets](
	[ParameterSetID] [int] NOT NULL,
	[Parameters] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ParameterSetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[processingRouter_ParametersSent]    Script Date: 28/10/2016 3:08:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[processingRouter_ParametersSent](
	[WorkerID] [uniqueidentifier] NOT NULL,
	[ParameterSetID] [int] NOT NULL,
	[Result] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ParameterSetID] ASC,
	[WorkerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[processingRouter_ParametersSent]  WITH CHECK ADD  CONSTRAINT [FK_ParametersSent_ParameterSets] FOREIGN KEY([ParameterSetID])
REFERENCES [dbo].[processingRouter_ParameterSets] ([ParameterSetID])
GO
ALTER TABLE [dbo].[processingRouter_ParametersSent] CHECK CONSTRAINT [FK_ParametersSent_ParameterSets]
GO
