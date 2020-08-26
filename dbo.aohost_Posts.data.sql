SET IDENTITY_INSERT [dbo].[aohost_Posts] ON
INSERT INTO [dbo].[aohost_Posts] ([Id], [Title], [Author], [Url], [Html], [Markdown], [CategoryId], [CreationTime]) 
VALUES 
(1, '动感超人', 'Aohost', 'www.baidu.com', '<h1>动感超人</h1>', '# 动感超人', 1, GETDATE()),
(2, '奥特曼', 'Aohost', 'www.qq.com', '<h1>奥特曼</h1>', '# 奥特曼', 1, GETDATE())
SET IDENTITY_INSERT [dbo].[aohost_Posts] OFF
