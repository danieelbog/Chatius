export interface ChatUser {
    id: string,
    name: string
}

export interface ChatGroup {
    id: string,
    memebers: Array<ChatUser>,
    creatorId: string,
    creator: ChatUser
}

export interface ChatMessage {
    id: string,
    chatGroupId: string,
    chatGroupd: ChatGroup,
    authorId: string
    author: ChatUser,
    content: string,
}