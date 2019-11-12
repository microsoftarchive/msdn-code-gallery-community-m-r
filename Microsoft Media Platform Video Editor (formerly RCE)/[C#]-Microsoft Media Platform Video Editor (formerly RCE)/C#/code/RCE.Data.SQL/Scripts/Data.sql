USE RoughCutEditor
GO

INSERT [dbo].[Container] ([Id], [Title], [ParentContainerId]) VALUES (1, N' MediaLibrary', NULL)

INSERT [dbo].[Item] ([Id], [Title], [Description], [ItemType]) VALUES (1, N'Cypress Gardens', N'', N'Video')
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (1, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/cypressgardens/CypressGardens_VC1_EE4_VBR_1080p_Xbox.ism/manifest', N'SmoothStream', 1)
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (2, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/thumbnails/cypressgarden.png', N'Thumbnail', 1)
INSERT [dbo].[VideoFormat] ([Id], [Duration], [FrameRate], [ResolutionX], [ResolutionY]) VALUES (1, 301.03, N'Smpte2997NonDrop', 320, 240)

INSERT [dbo].[Item] ([Id], [Title], [Description], [ItemType]) VALUES (2, N'Elephants Dream', N'', N'Video')
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (3, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/ElephantsDream/ElephantsDream_VC1_EE4_VBR_1080p_Xbox.ism/manifest', N'SmoothStream', 2)
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (4, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/thumbnails/elephantsdream.png', N'Thumbnail', 2)
INSERT [dbo].[VideoFormat] ([Id], [Duration], [FrameRate], [ResolutionX], [ResolutionY]) VALUES (2, 0, N'Smpte2997NonDrop', 320, 240)

INSERT [dbo].[Item] ([Id], [Title], [Description], [ItemType]) VALUES (3, N'Hawaii Surfing', N'', N'Video')
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (5, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/HawaiiSurfing/HawaiiSurfing_VC1_EE4_CBR_1080p_Xbox.ism/manifest', N'SmoothStream', 3)
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (6, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/thumbnails/hawaiisurfing.png', N'Thumbnail', 3)
INSERT [dbo].[VideoFormat] ([Id], [Duration], [FrameRate], [ResolutionX], [ResolutionY]) VALUES (3, 0, N'Smpte2997NonDrop', 320, 240)

INSERT [dbo].[Item] ([Id], [Title], [Description], [ItemType]) VALUES (4, N'India Taj Majal', N'', N'Video')
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (7, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/IndiaTajMajal/IndiaTajMajal_VC1_EE4_VBR_1080p_Xbox.ism/manifest', N'SmoothStream', 4)
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (8, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/thumbnails/indiatajmajal.png', N'Thumbnail', 4)
INSERT [dbo].[VideoFormat] ([Id], [Duration], [FrameRate], [ResolutionX], [ResolutionY]) VALUES (4, 0, N'Smpte2997NonDrop', 320, 240)

INSERT [dbo].[Item] ([Id], [Title], [Description], [ItemType]) VALUES (5, N'Irish Horse Racing', N'', N'Video')
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (9, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/IrishHorseRacing/IrishHorseRacing_VC1_EE4_VBR_1080p_Xbox.ism/manifest', N'SmoothStream', 5)
INSERT [dbo].[Resource] ([Id], [Ref], [ResourceType], [ItemId]) VALUES (10, N'http://devplatem.vo.msecnd.net/RCEAzureDemo/thumbnails/irishorseracing.png', N'Thumbnail', 5)
INSERT [dbo].[VideoFormat] ([Id], [Duration], [FrameRate], [ResolutionX], [ResolutionY]) VALUES (5, 0, N'Smpte2997NonDrop', 320, 240)

INSERT [dbo].[Container_Items] ([ContainerId], [ItemId]) VALUES (1, 1)
INSERT [dbo].[Container_Items] ([ContainerId], [ItemId]) VALUES (1, 2)
INSERT [dbo].[Container_Items] ([ContainerId], [ItemId]) VALUES (1, 3)
INSERT [dbo].[Container_Items] ([ContainerId], [ItemId]) VALUES (1, 4)
INSERT [dbo].[Container_Items] ([ContainerId], [ItemId]) VALUES (1, 5)

GO