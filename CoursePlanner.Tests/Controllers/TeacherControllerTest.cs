﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoursePlanner.Controllers;
using CoursePlanner.Models;

namespace CoursePlanner.Tests.Controllers
{
    [TestClass]
    public class TeacherControllerTest
    {
        TeacherController TeacherController = new TeacherController();

        [TestMethod]
        public void GetAgeBirthdayAfterNowTest()
        {
            //arange
            var dateOfBirth = new DateTime(2000, 12, 24);
            var target = 16;

            //act
            var result = TeacherController.GetAge(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBirthdayBeforeNowTest()
        {

            //arrange
            var dateOfBirth = new DateTime(2000, 06, 24);
            var target = 17;

            //act
            var result = TeacherController.GetAge(dateOfBirth);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBaseAnnualHoursAbove40Test()
        {
            //arrange
            var dateOfBirth = new DateTime(1970, 05, 12);
            var target = 1700;

            //act
            var result = TeacherController.GetBaseAnnualHours(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBaseAnnualHoursBetween30and40Test()
        {
            //arrange
            var dateOfBirth = new DateTime(1980, 05, 12);
            var target = 1735;

            //act
            var result = TeacherController.GetBaseAnnualHours(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBaseAnnualHoursBelow30Test()
        {
            //arrange
            var dateOfBirth = new DateTime(1992, 05, 12);
            var target = 1756;

            //act
            var result = TeacherController.GetBaseAnnualHours(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetAgeBaseAnnualHoursAgeBolow0Test()
        {
            //arrange
            var dateOfBirth = new DateTime(2017, 12, 12);
            var target = 0;

            //act
            var result = TeacherController.GetBaseAnnualHours(dateOfBirth);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void TotalTeachingHoursAvailableFallTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01)
            };
            var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950, 01, 01));
            var target = baseHours / 2;


            //act
            var result = tc.TotalTeachingHoursTerm(teacher, Terms.Fall);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void TotalTeachingHoursAvailableSpringTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 001,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01)
            };
            var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950, 01, 01));
            var target = baseHours / 2;


            //act
            var result = tc.TotalTeachingHoursTerm(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void TotalTeachingHoursAvailableWithOneReductionFallTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>()
                 {
                     new TeacherReduction {
                     TeacherId = 1,
                     Term = Terms.Fall,
                     Percentage = 0.5
                     }
                 }
            };
            var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950, 01, 01));
            var target = (baseHours / 2) * (1 - 0.5);


            //act
            var result = tc.TotalTeachingHoursTerm(teacher, Terms.Fall);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void TotalTeachingHoursAvailableWithTwoReductionsFallTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>()
                 {
                     new TeacherReduction {
                     TeacherId = 1,
                     Term = Terms.Fall,
                     Percentage = 0.5
                     },
                     new TeacherReduction {
                     TeacherId = 1,
                     Term = Terms.Fall,
                     Percentage = 0.2
                     }
                 }
            };
            var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950, 01, 01));
            var target = Convert.ToInt32(Math.Round((baseHours / 2) * (1 - 0.7), MidpointRounding.AwayFromZero));


            //act
            var result = tc.TotalTeachingHoursTerm(teacher, Terms.Fall);

            //assert
            Assert.AreEqual(target, result);
        }


        [TestMethod]
        public void TotalTeachingHoursAvailableWithOneReductionSpringTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>()
                 {
                     new TeacherReduction {
                     TeacherId = 1,
                     Term = Terms.Spring,
                     Percentage = 0.5
                     }
                 }
            };
            var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950, 01, 01));
            var target = (baseHours / 2) * (1 - 0.5);


            //act
            var result = tc.TotalTeachingHoursTerm(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void TotalTeachingHoursAvailableWithTwoReductionsSpringTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>()
                 {
                     new TeacherReduction {
                     TeacherId = 1,
                     Term = Terms.Spring,
                     Percentage = 0.5
                     },
                     new TeacherReduction {
                     TeacherId = 1,
                     Term = Terms.Spring,
                     Percentage = 0.2
                     }
                 }
            };
            var baseHours = TeacherController.GetBaseAnnualHours(new DateTime(1950, 01, 01));
            var target = Convert.ToInt32(Math.Round((baseHours / 2) * (1 - 0.7), MidpointRounding.AwayFromZero));


            //act
            var result = tc.TotalTeachingHoursTerm(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target, result);
        }
        [TestMethod]
        public void TotalTeachingHoursAllocatedOneOccurrenceFallTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1990, 01, 01),
                CourseTeacher = new List<CourseTeacher>()
                 {
                     new CourseTeacher{
                         TeacherId= 1,
                         Hours = 50,
                         CourseOccurrenceId = 1,
                         CourseOccurrence = new CourseOccurrence{
                            Term = Terms.Fall,
                            Year = "2017/2018"
                         }},
                         
                     
                 }
            };

            var target = 50;


            //act
            var result = tc.TeachingHoursAllocatedTerm(teacher, Terms.Fall);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void TotalTeachingHoursAllocatedTwoOccurrencesFallTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                CourseTeacher = new List<CourseTeacher>()
                 {
                     new CourseTeacher{
                         TeacherId= 1,
                         Hours = 50,
                         CourseOccurrenceId = 1,
                         CourseOccurrence = new CourseOccurrence{
                            Term = Terms.Fall,
                            Year = "2017/2018"
                         }},
                         new CourseTeacher{
                         TeacherId= 1,
                         Hours = 100,
                         CourseOccurrenceId = 2,
                         CourseOccurrence = new CourseOccurrence{
                            Term = Terms.Fall,
                            Year = "2017/2018"
                         }
                     }
                 }
            };

            var target = 150;


            //act
            var result = tc.TeachingHoursAllocatedTerm(teacher, Terms.Fall);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void TotalTeachingHoursAllocatedOneOccurrenceSpringTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                CourseTeacher = new List<CourseTeacher>()
                 {
                     new CourseTeacher{
                         TeacherId= 1,
                         Hours = 150,
                         CourseOccurrenceId = 1,
                         CourseOccurrence = new CourseOccurrence{
                            Term = Terms.Spring,
                            Year = "2017/2018"
                         }},
                 }
            };

            var target = 150;


            //act
            var result = tc.TeachingHoursAllocatedTerm(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void TotalTeachingHoursAllocatedTwoOccurrencesSpringTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                CourseTeacher = new List<CourseTeacher>()
                 {
                     new CourseTeacher{
                         TeacherId= 1,
                         Hours = 150,
                         CourseOccurrenceId = 1,
                         CourseOccurrence = new CourseOccurrence{
                            Term = Terms.Spring,
                            Year = "2017/2018"
                         }},
                         new CourseTeacher{
                         TeacherId= 1,
                         Hours = 100,
                         CourseOccurrenceId = 2,
                         CourseOccurrence = new CourseOccurrence{
                            Term = Terms.Spring,
                            Year = "2017/2018"
                         }
                     }
                 }
            };

            var target = 250;

            //act
            var result = tc.TeachingHoursAllocatedTerm(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void TotalTeachingHoursAllocatedNoOccurrencesTest()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01)
            };

            var target = 0;

            //act
            var result = tc.TeachingHoursAllocatedTerm(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void HoursAllocatedPerReductionAdministrationFall()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>
                {
                    new TeacherReduction
                    {
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Administration,
                        TeacherId = 1,
                        Term = Terms.Fall
                    }
                }
            };
            var target = (int)(Math.Round((decimal)((1700/2 * 1) * 0.1), 0, MidpointRounding.AwayFromZero));

            //act
            var result = tc.HoursAllocatedPerReduction(teacher, Terms.Fall, ReductionTypes.Administration);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void HoursAllocatedPerReductionAdministrationSpring()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>
                {
                    new TeacherReduction
                    {
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Administration,
                        TeacherId = 1,
                        Term = Terms.Spring
                    }
                }
            };
            var target = (int)(Math.Round((decimal)((1700 / 2 * 1) * 0.1), 0, MidpointRounding.AwayFromZero));

            //act
            var result = tc.HoursAllocatedPerReduction(teacher, Terms.Spring, ReductionTypes.Administration);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void HoursAllocatedPerReductionAdministrationMoreThanOneFall()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>
                {
                    new TeacherReduction
                    {
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Assignments,
                        TeacherId = 1,
                        Term = Terms.Fall
                    },
                    new TeacherReduction
                    {
                        Percentage = 0.3,
                        ReductionType = ReductionTypes.Assignments,
                        TeacherId = 1,
                        Term = Terms.Fall
                    }
                }
            };
            var target = (int)(Math.Round((decimal)((1700 / 2 * 1) * 0.4), 0, MidpointRounding.AwayFromZero));

            //act
            var result = tc.HoursAllocatedPerReduction(teacher, Terms.Fall, ReductionTypes.Assignments);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void HoursAllocatedPerReductionAdministrationMoreThanOneSpring()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>
                {
                    new TeacherReduction
                    {
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Assignments,
                        TeacherId = 1,
                        Term = Terms.Spring
                    },
                    new TeacherReduction
                    {
                        Percentage = 0.3,
                        ReductionType = ReductionTypes.Assignments,
                        TeacherId = 1,
                        Term = Terms.Spring
                    }
                }
            };
            var target = (int)(Math.Round((decimal)((1700 / 2 * 1) * 0.4), 0, MidpointRounding.AwayFromZero));

            //act
            var result = tc.HoursAllocatedPerReduction(teacher, Terms.Spring, ReductionTypes.Assignments);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void HoursAllocatedPerReductionNoReductions()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01)
            };

            var target = 0;

            //act
            var result = tc.HoursAllocatedPerReduction(teacher, Terms.Spring, ReductionTypes.Administration);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionPercentageByTypeAdministration()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Administration,
                        TeacherId = 1,
                        Term = Terms.Fall
                    }}
            };

            var target = 0.1;

            //act
            var result = tc.GetReductionByTypePercentage(teacher, Terms.Fall, ReductionTypes.Administration);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionPercentageByTypeAssignments()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Assignments,
                        TeacherId = 1,
                        Term = Terms.Fall
                    }}
            };

            var target = 0.1;

            //act
            var result = tc.GetReductionByTypePercentage(teacher, Terms.Fall, ReductionTypes.Assignments);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionPercentageByTypeOther()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Other,
                        TeacherId = 1,
                        Term = Terms.Fall
                    }}
            };

            var target = 0.1;

            //act
            var result = tc.GetReductionByTypePercentage(teacher, Terms.Fall, ReductionTypes.Other);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionPercentageByTypeResearch()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Research,
                        TeacherId = 1,
                        Term = Terms.Fall
                    }}
            };

            var target = 0.1;

            //act
            var result = tc.GetReductionByTypePercentage(teacher, Terms.Fall, ReductionTypes.Research);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionPercentageByTypeAdministrationMoreThanOne()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Administration,
                        TeacherId = 1,
                        Term = Terms.Fall
                    },
                        new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Administration,
                        TeacherId = 1,
                        Term = Terms.Fall
            }}
            };

            var target = 0.2;

            //act
            var result = tc.GetReductionByTypePercentage(teacher, Terms.Fall, ReductionTypes.Administration);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionPercentageByTypeAdministrationMoreThanOneSpring()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Administration,
                        TeacherId = 1,
                        Term = Terms.Spring
                    },
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Administration,
                        TeacherId = 1,
                        Term = Terms.Spring
                    }}
            };

            var target = 0.2;

            //act
            var result = tc.GetReductionByTypePercentage(teacher, Terms.Spring, ReductionTypes.Administration);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionPercentageNoReductions()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01)
            };

            var target = 0.0;

            //act
            var result = tc.GetReductionByTypePercentage(teacher, Terms.Fall, ReductionTypes.Research);

            //assert
            Assert.AreEqual(target, result);
        }
        
        [TestMethod]
        public void GetReductionByPercentageFall()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Assignments,
                        TeacherId = 1,
                        Term = Terms.Fall
                    }
                    }
            };

            var target = 0.1;

            //act
            var result = tc.GetTotalReductionPercentage(teacher, Terms.Fall);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionByPercentageMoreThanOneTypeFall()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Assignments,
                        TeacherId = 1,
                        Term = Terms.Fall
                    },
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Research,
                        TeacherId = 1,
                        Term = Terms.Fall
                    },
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Other,
                        TeacherId = 1,
                        Term = Terms.Fall
                    }
                }
            };

            var target = 0.3;

            //act
            var result = tc.GetTotalReductionPercentage(teacher, Terms.Fall);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionByPercentageSpring()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Assignments,
                        TeacherId = 1,
                        Term = Terms.Spring
                    }
                }
            };

            var target = 0.1;

            //act
            var result = tc.GetTotalReductionPercentage(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionByPercentageMoreThanOneTypeSpring()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>{
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Assignments,
                        TeacherId = 1,
                        Term = Terms.Spring
                    },
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Research,
                        TeacherId = 1,
                        Term = Terms.Spring
                    },
                    new TeacherReduction{
                        Percentage = 0.1,
                        ReductionType = ReductionTypes.Other,
                        TeacherId = 1,
                        Term = Terms.Spring
                    }
                }
            };

            var target = 0.3;

            //act
            var result = tc.GetTotalReductionPercentage(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void GetReductionByPercentageNoReductions()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01)
            };

            var target = 0.0;

            //act
            var result = tc.GetTotalReductionPercentage(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void HoursInPeriodsInTermTest()
        {
            var courseOccurence1 = new CourseOccurrence()
            {
                Term = Terms.Spring,
                Year = "2017/2018",
                Period = Periods.P1
            };

            var courseOccurence2 = new CourseOccurrence()
            {
                Term = Terms.Spring,
                Year = "2017/2018",
                Period = Periods.P2
            };

            var courseOccurence3 = new CourseOccurrence()
            {
                Term = Terms.Spring,
                Year = "2017/2018",
                Period = Periods.P3
            };

            var courseOccurence4 = new CourseOccurrence()
            {
                Term = Terms.Spring,
                Year = "2017/2018",
                Period = Periods.P4
            };

            var courseTeacher1 = new CourseTeacher
            {
                TeacherId = 1,
                Hours = 100,
                CourseOccurrenceId = 1,
                CourseOccurrence = courseOccurence1
            };
            courseOccurence1.CourseTeacher = new List<CourseTeacher>{courseTeacher1};

            var courseTeacher2 = new CourseTeacher
            {
                TeacherId = 1,
                Hours = 100,
                CourseOccurrenceId = 1,
                CourseOccurrence = courseOccurence2
            };
            courseOccurence2.CourseTeacher = new List<CourseTeacher>{courseTeacher2};
            
            var courseTeacher3 = new CourseTeacher
            {
                TeacherId = 1,
                Hours = 100,
                CourseOccurrenceId = 1,
                CourseOccurrence = courseOccurence3
            };
            courseOccurence3.CourseTeacher = new List<CourseTeacher>{courseTeacher3};

            var courseTeacher4 = new CourseTeacher
            {
                TeacherId = 1,
                Hours = 100,
                CourseOccurrenceId = 1,
                CourseOccurrence = courseOccurence4
            };
            courseOccurence4.CourseTeacher = new List<CourseTeacher>{courseTeacher4};

            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                CourseTeacher = new List<CourseTeacher>
                {
                    courseTeacher1,
                    courseTeacher2,
                    courseTeacher3,
                    courseTeacher4 }
            };

            courseTeacher1.Teacher = teacher;
            courseTeacher2.Teacher = teacher;
            courseTeacher3.Teacher = teacher; 
            courseTeacher4.Teacher = teacher;

            var target = new []{ 100,100,100,100};

            //act
            var result = tc.HoursInPeriodsOnTerm(teacher, Terms.Spring);

            //assert
            Assert.AreEqual(target[0], result[0]);
            Assert.AreEqual(target[1], result[1]);
            Assert.AreEqual(target[2], result[2]);
            Assert.AreEqual(target[3], result[3]);
        }

        [TestMethod]
        public void BalanceInTermTestFall()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                CourseTeacher = new List<CourseTeacher>()
                {
                    new CourseTeacher
                    {
                        TeacherId = 1,
                        Hours = 100,
                        CourseOccurrenceId = 1,
                        CourseOccurrence = new CourseOccurrence
                        {
                            Term = Terms.Fall,
                            Year = "2017/2018"
                        }
                    },
                    new CourseTeacher
                    {
                        TeacherId = 1,
                        Hours = 100,
                        CourseOccurrenceId = 2,
                        CourseOccurrence = new CourseOccurrence
                        {
                            Term = Terms.Fall,
                            Year = "2017/2018"
                        }
                    }
                }
            };


            var allocated = 200;
            var available = 1700 / 2;

            var target = available - allocated;

            //act
            var result = tc.BalanceInTerm(teacher, Terms.Fall);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void BalanceInTermTestSpring()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                CourseTeacher = new List<CourseTeacher>()
                {
                    new CourseTeacher
                    {
                        TeacherId = 1,
                        Hours = 100,
                        CourseOccurrenceId = 1,
                        CourseOccurrence = new CourseOccurrence
                        {
                            Term = Terms.Spring,
                            Year = "2017/2018"
                        }
                    },
                    new CourseTeacher
                    {
                        TeacherId = 1,
                        Hours = 100,
                        CourseOccurrenceId = 2,
                        CourseOccurrence = new CourseOccurrence
                        {
                            Term = Terms.Spring,
                            Year = "2017/2018"
                        }
                    }
                }
            };


            var allocated = 200;
            var available = 1700 / 2;

            var target = available - allocated;

            //act
            var result = tc.BalanceInTerm(teacher, Terms.Spring);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void BalanceInTermTestSpringWithReduction()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageSpring = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                TeacherReduction = new List<TeacherReduction>
                {
                    new TeacherReduction()
                    {
                        Term = Terms.Spring,
                        Percentage = 0.2
                    }
                },
                CourseTeacher = new List<CourseTeacher>()
                {
                    new CourseTeacher
                    {
                        TeacherId = 1,
                        Hours = 100,
                        CourseOccurrenceId = 1,
                        CourseOccurrence = new CourseOccurrence
                        {
                            Term = Terms.Spring,
                            Year = "2017/2018"
                        }
                    },
                    new CourseTeacher
                    {
                        TeacherId = 1,
                        Hours = 100,
                        CourseOccurrenceId = 2,
                        CourseOccurrence = new CourseOccurrence
                        {
                            Term = Terms.Spring,
                            Year = "2017/2018"
                        }
                    }
                }
            };


            var allocated = 200;
            var available = Math.Round((double)(1700 / 2) * (1 - 0.2), 0, MidpointRounding.AwayFromZero);

            var target = available - allocated;

            //act
            var result = tc.BalanceInTerm(teacher, Terms.Spring);
            //assert
            Assert.AreEqual(target, result);
        }

        [TestMethod]
        public void BalanceInTermTestFallNegativeBalance()
        {
            //arrange
            var tc = new TeacherController();
            var teacher = new Teacher()
            {
                TeacherId = 1,
                TeacherName = "Teacher",
                TotalPercentageFall = 1,
                TeacherDateOfBirth = new DateTime(1950, 01, 01),
                CourseTeacher = new List<CourseTeacher>()
                {
                    new CourseTeacher
                    {
                        TeacherId = 1,
                        Hours = 500,
                        CourseOccurrenceId = 1,
                        CourseOccurrence = new CourseOccurrence
                        {
                            Term = Terms.Fall,
                            Year = "2017/2018"
                        }
                    },
                    new CourseTeacher
                    {
                        TeacherId = 1,
                        Hours = 500,
                        CourseOccurrenceId = 2,
                        CourseOccurrence = new CourseOccurrence
                        {
                            Term = Terms.Fall,
                            Year = "2017/2018"
                        }
                    }
                }
            };


            var allocated = 1000;
            var available = 1700 / 2;

            var target = available - allocated;

            //act
            var result = tc.BalanceInTerm(teacher, Terms.Fall);
            //assert
            Assert.AreEqual(target, result);
        }


    }

}
