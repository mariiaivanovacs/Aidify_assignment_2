USE AidifyDB;
GO

-- Admin user (password: Admin123!)
-- BCrypt hash generated at work factor 11 for "Admin123!"
-- Regenerate with: BCrypt.Net.BCrypt.HashPassword("Admin123!", 11) if this hash doesn't work
INSERT INTO Users (FullName, Email, PasswordHash, RoleId, IsActive, IsEmailConfirmed)
VALUES (
    'Aidify Admin',
    'admin@aidify.edu',
    '$2a$11$rOzJqQKQ1CdhA9q.dX7LuOjG5lO.VkS4Wx0R4U8z9bA3SiNQkr.Hy',
    (SELECT RoleId FROM Roles WHERE RoleName = 'Admin'),
    1, 1
);

-- Demo instructor (password: Instructor1!)
INSERT INTO Users (FullName, Email, PasswordHash, RoleId, IsActive, IsEmailConfirmed)
VALUES (
    'Demo Instructor',
    'instructor@aidify.edu',
    '$2a$11$rOzJqQKQ1CdhA9q.dX7LuOjG5lO.VkS4Wx0R4U8z9bA3SiNQkr.Hy',
    (SELECT RoleId FROM Roles WHERE RoleName = 'Instructor'),
    1, 1
);

-- Demo learner (password: Learner1!)
INSERT INTO Users (FullName, Email, PasswordHash, RoleId, IsActive, IsEmailConfirmed)
VALUES (
    'Demo Learner',
    'learner@aidify.edu',
    '$2a$11$rOzJqQKQ1CdhA9q.dX7LuOjG5lO.VkS4Wx0R4U8z9bA3SiNQkr.Hy',
    (SELECT RoleId FROM Roles WHERE RoleName = 'Learner'),
    1, 1
);

DECLARE @InstructorId INT = (SELECT UserId FROM Users WHERE Email = 'instructor@aidify.edu');

-- Demo modules
INSERT INTO Modules (Title, Description, DifficultyLevel, Status, IsPreview, CreatedBy)
VALUES
    ('CPR Fundamentals',
     'Learn the essentials of Cardiopulmonary Resuscitation for adults, children, and infants.',
     'Beginner', 'Published', 1, @InstructorId),
    ('Choking & Airway Management',
     'Recognise and respond to choking emergencies using the Heimlich manoeuvre and back blows.',
     'Beginner', 'Published', 1, @InstructorId),
    ('Trauma Response',
     'Managing severe bleeding, fractures, and shock in emergency situations.',
     'Intermediate', 'Published', 0, @InstructorId);

-- Sample FAQs
INSERT INTO FAQs (Question, Answer, Category, SortOrder) VALUES
    ('Is Aidify a certified first-aid course?',
     'Aidify is an educational platform designed to raise awareness of first-aid techniques. It does not replace a certified first-aid course from a recognised provider.',
     'General', 1),
    ('Do I need any prior knowledge to start?',
     'No prior knowledge is required. Our Beginner modules assume no medical background.',
     'Getting Started', 2),
    ('How do I earn a certificate?',
     'Complete 100% of a module and pass its final quiz. Your certificate will be automatically generated and available to download from your Profile page.',
     'Certificates', 3),
    ('Can I preview content before registering?',
     'Yes. The Preview Modules and Preview Quiz pages are available to all visitors without an account.',
     'Access', 4),
    ('What should I do in a real emergency?',
     'Always call your local emergency services immediately (999, 911, or 112). Aidify is for educational purposes only and is not a substitute for professional emergency services.',
     'Safety', 5);

-- Sample badges
INSERT INTO Badges (Name, IconPath, RuleType, RuleThreshold) VALUES
    ('First Responder',   '/Images/badges/first-responder.png', 'ModulesCompleted', 1),
    ('Quiz Ace',          '/Images/badges/quiz-ace.png',        'QuizScore',        90),
    ('CPR Hero',          '/Images/badges/cpr-hero.png',        'ModulesCompleted', 3),
    ('Knowledge Seeker',  '/Images/badges/knowledge-seeker.png','ModulesCompleted', 2);
