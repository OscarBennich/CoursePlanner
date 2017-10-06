SET IDENTITY_INSERT [dbo].[CourseOccurrence] ON
INSERT INTO [dbo].[CourseOccurrence] ([CourseOccurrenceID], [Year], [Term], [Period], [Budget], [NoOfStudents], [NoOfPassingStudents], [HST], [StartDate], [EndDate], [Status], [CourseID], [CourseResponsibleID]) VALUES (9, N'2016-2017', 0, 0, 40, 20, 20, 30, N'2016-09-01', N'2016-09-30', 2, 1, 3);
INSERT INTO [dbo].[CourseOccurrence] ([CourseOccurrenceID], [Year], [Term], [Period], [Budget], [NoOfStudents], [NoOfPassingStudents], [HST], [StartDate], [EndDate], [Status], [CourseID], [CourseResponsibleID]) VALUES (10, N'2017-2018', 0, 0, 40, 15, 5, 45, N'2017-09-01', N'2017-10-30', 1, 1,NULL );




SET IDENTITY_INSERT [dbo].[CourseOccurrence] OFF