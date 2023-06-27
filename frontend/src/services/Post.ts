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
    id : number
    author : number
    title : string
    content : string
    createdAt : Date
    crowds : number
    comments : number
    idPost : number
    idForum : number
}