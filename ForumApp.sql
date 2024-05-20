CREATE DATABASE DBforum;

USE DBforum;

DROP DATABASE DBforum;

CREATE TABLE users (
    id_user INT IDENTITY PRIMARY KEY,
    username NVARCHAR(50) UNIQUE,
    email NVARCHAR(100) UNIQUE,
    password NVARCHAR(100),
	pin VARCHAR(6),
    level INT CHECK (level IN (1, 2))
);

ALTER TABLE users 
ADD CONSTRAINT CHK_PinFormat 
CHECK (LEN(pin) = 6 AND pin NOT LIKE '%[^0-9]%');

CREATE TABLE ban (
    id_ban INT IDENTITY PRIMARY KEY,
    user_id INT,
    start_date DATE DEFAULT GETDATE(),
    end_date DATE,
    FOREIGN KEY (user_id) REFERENCES users(id_user) ON DELETE CASCADE
); 

ALTER TABLE posts
ADD CONSTRAINT CHK_LikeCount CHECK (like_count >= 0);

ALTER TABLE posts
ADD CONSTRAINT df_edited
DEFAULT 0 FOR edited;

CREATE TABLE comments (
    id_comment INT IDENTITY PRIMARY KEY,
    user_id INT FOREIGN KEY (user_id) REFERENCES users(id_user),
    post_id INT FOREIGN KEY (post_id) REFERENCES posts(id_post) ON DELETE CASCADE,
    description TEXT,
    comment_date DATETIME,
	edited BIT DEFAULT 0,
);


CREATE TABLE shared_posts (
	id_share INT IDENTITY PRIMARY KEY,
    user_id INT,
    post_id INT,
    FOREIGN KEY (user_id) REFERENCES users(id_user),
    FOREIGN KEY (post_id) REFERENCES posts(id_post) ON DELETE CASCADE
);

CREATE TABLE liked_posts (
	id_like INT IDENTITY PRIMARY KEY,
    post_id INT,
    user_id INT,
    FOREIGN KEY (post_id) REFERENCES posts(id_post) ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id_user)
);

CREATE TABLE reports (
    id_report INT IDENTITY PRIMARY KEY,
    reporter_id INT NULL,
    post_id INT NULL,
    comment_id INT,
    description TEXT,
    FOREIGN KEY (reporter_id) REFERENCES users(id_user),
    FOREIGN KEY (post_id) REFERENCES posts(id_post),
    FOREIGN KEY (comment_id) REFERENCES comments(id_comment) ON DELETE CASCADE
);

SELECT * FROM users;
SELECT * FROM ban;
SELECT * FROM posts;
SELECT * FROM reports;
SELECT * FROM posts;
SELECT * FROM comments;
SELECT * FROM liked_posts;
SELECT * FROM shared_posts;

DELETE FROM users WHERE username='arb123';

SELECT * FROM posts;

DELETE FROM ban WHERE id_ban = 2;

UPDATE comments SET edited = 0 WHERE edited IS NULL;

-- Dummy data for users table with Gmail addresses and varied usernames
INSERT INTO users (username, email, password, pin, level) VALUES
('john_doe', 'john.doe@gmail.com', 'password1', '123456', 1),
('jane_smith', 'jane.smith@gmail.com', 'password2', '654321', 2),
('emma_jones', 'emma.jones@gmail.com', 'password3', '987654', 1),
('michael_brown', 'michael.brown@gmail.com', 'password4', '246810', 1),
('sarah_adams', 'sarah.adams@gmail.com', 'password5', '135790', 2),
('david_wilson', 'david.wilson@gmail.com', 'password6', '112233', 1);

-- Dummy data for posts table
INSERT INTO posts (title, description, post_date, user_id) VALUES
('First Post', 'This is the first post.', '2024-04-17', 1),
('Second Post', 'This is the second post.', '2024-04-18', 2),
('Third Post', 'This is the third post.', '2024-04-19', 3),
('Fourth Post', 'This is the fourth post.', '2024-04-20', 4),
('Fifth Post', 'This is the fifth post.', '2024-04-21', 5),
('Sixth Post', 'This is the sixth post.', '2024-04-22', 6);

-- Dummy data for comments table
INSERT INTO comments (user_id, post_id, description, comment_date) VALUES
(1, 2, 'Nice post!', '2024-04-18 11:00:00'),
(2, 3, 'Thanks for sharing!', '2024-04-19 09:00:00'),
(3, 3, 'Keep it up!', '2024-04-19 09:30:00'),
(4, 4, 'Great job!', '2024-04-20 10:00:00'),
(5, 5, 'Interesting!', '2024-04-21 11:00:00'),
(6, 6, 'Looking forward to more!', '2024-04-22 12:00:00');