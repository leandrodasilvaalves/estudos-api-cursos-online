
INSERT INTO AspNetUserClaims
  (UserId, ClaimType, ClaimValue)
VALUES
  (
    'c0d782ea-0353-4d58-c0ea-08d8a9d8e442',
    'Curso', 'Inc,Edit'
)

SELECT *
FROM [CursosOnline].[dbo].[AspNetUsers]
WHERE Email = 'leandro@teste.com'
SELECT *
FROM [CursosOnline].[dbo].[AspNetUserClaims]