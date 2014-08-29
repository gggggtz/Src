/****** Object:  Table [dbo].[Element]    Script Date: 3/11/2013 1:36:21 PM ******/
CREATE TABLE [dbo].[Element](
	[Id] [uniqueidentifier] NOT NULL,
	[ReservedBy] [uniqueidentifier] NULL,
	[State] [tinyint] NOT NULL,
	[Discriminator] int NOT NULL,
	[Revision] bigint NOT NULL,
	[LastModified] DateTime NULL,
	[Created] DateTime NULL,
 CONSTRAINT [PK_dbo.Element] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]