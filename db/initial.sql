USE [PaymentNotifier]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 16.03.2015 1:34:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[NotificationSettingsId] [int] NOT NULL,
	[Value] [int] NOT NULL,
	[SentOn] [date] NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NotificationSettings]    Script Date: 16.03.2015 1:34:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NotificationSettings](
	[NotificationSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[Pattern] [nvarchar](1000) NOT NULL,
	[Subject] [nvarchar](250) NOT NULL,
	[AverageConsumtion] [int] NOT NULL,
	[DefaultStartValue] [int] NOT NULL,
	[Addressee] [varchar](250) NOT NULL,
 CONSTRAINT [PK_NotificationSettings] PRIMARY KEY CLUSTERED 
(
	[NotificationSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[NotificationSettings] ON 

INSERT [dbo].[NotificationSettings] ([NotificationSettingsId], [Pattern], [Subject], [AverageConsumtion], [DefaultStartValue], [Addressee]) VALUES (1, N'Показания счетчик на {0: dd.MM.yy} составили {1}', N'Показания газового счетчика. Златоустовская 23, кв 21', 50, 940, N'kozyrev.vitaliy@gmail.com')
SET IDENTITY_INSERT [dbo].[NotificationSettings] OFF
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_NotificationSettings] FOREIGN KEY([NotificationSettingsId])
REFERENCES [dbo].[NotificationSettings] ([NotificationSettingsId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_NotificationSettings]
GO
