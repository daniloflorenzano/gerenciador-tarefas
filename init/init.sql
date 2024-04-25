-- noinspection SqlNoDataSourceInspectionForFile
create table TaskManager.dbo.Topic
(
    Id    int identity
        constraint Topic_pk
            primary key,
    Name  varchar(20) not null,
    Color varchar(20)
);

create table TaskManager.dbo.[User]
(
    Id         int identity
    constraint User_pk
    primary key,
    Name       varchar(100)               not null,
    Created_At datetime default getdate() not null,
    Updated_At datetime
    );

create table TaskManager.dbo.Comment
(
    Id                  int identity
        constraint Comment_pk
            primary key,
    Text_Content        varchar(300)               not null,
    Created_At          datetime default getdate() not null,
    Updated_At          datetime default getdate() not null,
    User_Id             int                        not null
        constraint Comment_User_Id_fk
            references dbo.[User],
    Previous_Comment_Id int
        constraint Comment_Comment_Id_fk
            references dbo.Comment
);

create table TaskManager.dbo.Task
(
    Id           int identity
        constraint Task_pk
            primary key,
    Title        varchar(300)                not null,
    Text_Content varchar(1000)               not null,
    User_Id      int                        not null
        constraint Task_User_Id_fk
            references dbo.[User],
    Topic_Id     int                        not null
        constraint Task_Topic_Id_fk
            references dbo.Topic,
    Status       int                        not null,
    Created_At   datetime default getdate() not null,
    Updated_At   datetime
);