using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizletWebAPI.Auth.Models;

namespace QuizletWebAPI.Auth.Configuration
{
    public class TestsConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            _ = builder.ToTable("Tests");

            var questions = new Question[]
            {
            new Question { Number = 1, Body = "Which of the following does TypeScript use to specify types?" },
            new Question { Number = 2, Body = "Which of the following is NOT a type used in TypeScript?"},
            new Question { Number = 3, Body = "How can we specify properties and methods for an object in TypeScript?"},
            new Question { Number = 4, Body = "How else can Array<number> be written in TypeScript?"},
            new Question { Number = 5, Body = "In which of these does a class take parameters?"},
            new Question { Number = 6, Body = "Which is NOT an access modifier?"},
            new Question { Number = 7, Body = "Which keyword allows us to share information between files in TypeScript?"},
            new Question { Number = 8, Body = "Which is an array method to generate a new array based on a condition?"},
            new Question { Number = 9, Body = "How is a property accessible within a class?"}
            };

            var answers = new Answer[]
            {
            new Answer {QuestionNumber = 1, BodyAnswer = new BodyAnswer[]
                {
                    new BodyAnswer {Text = ":", IsCorrect = true},
                    new BodyAnswer {Text = ";"},
                    new BodyAnswer {Text = "!"}
                }
            },
            new Answer {QuestionNumber = 2, BodyAnswer = new BodyAnswer[]
                {
                    new BodyAnswer {Text = "string"},
                    new BodyAnswer {Text = "boolean"},
                    new BodyAnswer {Text = "enum", IsCorrect = true}
                }
            },
            new Answer {QuestionNumber = 3, BodyAnswer = new BodyAnswer[]
                {
                    new BodyAnswer {Text = "Use classes."},
                    new BodyAnswer {Text = "Use interfaces.", IsCorrect = true},
                    new BodyAnswer {Text = "Use enums."}
                }
            },
            new Answer {QuestionNumber = 4, BodyAnswer = new BodyAnswer[]
                {
                    new BodyAnswer {Text = "@number"},
                    new BodyAnswer {Text = "number[]", IsCorrect = true},
                    new BodyAnswer {Text = "number?"}
                }
            },
            new Answer {QuestionNumber = 5, BodyAnswer = new BodyAnswer[]
                {
                    new BodyAnswer {Text = "constructor", IsCorrect = true},
                    new BodyAnswer {Text = "destructor"},
                    new BodyAnswer {Text = "import"}
                }
            },
            new Answer {QuestionNumber = 6, BodyAnswer = new BodyAnswer[]
                {
                    new BodyAnswer {Text = "protected"},
                    new BodyAnswer {Text = "public"},
                    new BodyAnswer {Text = "async", IsCorrect = true}
                }
            },
            new Answer {QuestionNumber = 7, BodyAnswer = new BodyAnswer[]
                {
                    new BodyAnswer {Text = "constructor"},
                    new BodyAnswer {Text = "import"},
                    new BodyAnswer {Text = "export", IsCorrect = true}
                }
            },
            new Answer {QuestionNumber = 8, BodyAnswer = new BodyAnswer[]
                {
                    new BodyAnswer {Text = "filter", IsCorrect = true},
                    new BodyAnswer {Text = "map"},
                    new BodyAnswer {Text = "enum"}
                }
            },
            new Answer {QuestionNumber = 9, BodyAnswer = new BodyAnswer[]
                {
                    new BodyAnswer {Text = "Arrow function"},
                    new BodyAnswer {Text = "Accessors"},
                    new BodyAnswer {Text = "Using this.propertyName", IsCorrect = true}
                }
            }
            };

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var questionsJson = JsonSerializer.Serialize(questions, serializeOptions);
            var answersJson = JsonSerializer.Serialize(answers, serializeOptions);

            _ = builder.HasData(
                new Test()
                {
                    Id = 1,
                    Name = "Test 1",
                    Description = "TypeScript knowledge test",
                    Questions = questionsJson,
                    Answers = answersJson
                },
                new Test()
                {
                    Id = 2,
                    Name = "Test 2",
                    Description = "TypeScript knowledge test",
                    Questions = questionsJson,
                    Answers = answersJson
                },
                new Test()
                {
                    Id = 3,
                    Name = "Test 3",
                    Description = "TypeScript knowledge test",
                    Questions = questionsJson,
                    Answers = answersJson
                },
                new Test()
                {
                    Id = 4,
                    Name = "Test 4",
                    Description = "TypeScript knowledge test",
                    Questions = questionsJson,
                    Answers = answersJson
                }
                );
        }
    }
}
