export interface ChatUser {
    id: string,
    name: string
}

export interface ChatGroup {
    id: string,
    memebers: Array<string>,
    creatorId: string
}

export interface ChatMessage {
    chatGroupId: string,
    authorId: string,
    content: string,
}