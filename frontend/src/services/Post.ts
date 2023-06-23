export interface CompletePost
{
    idAuthor : number
    authorName : string
    content : string
    createdAt : Date
    crowds : number
    comments : number
    idPost : number
    photo : string
    idForum : number
    forumName : string
    fkPost : number
    title : string
}

export interface Post
{
    Id : number
    Author : number
    Title : string
    Content : string
    CreatedAt : Date
    Crowds : number
    Comments : number
    IdPost : number
    IdForum : number
}