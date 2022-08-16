import { ChatMessage, ChatUser } from './chat.service.dto';
var uuid = require("uuid");
const myId = uuid.v4();

export async function getUsers(userid: string): Promise<Array<ChatUser>>{
    const chatUsers = [] as Array<ChatUser>
    setTimeout(() => {
        for (var i = 0; i < 25; i++) {
            var userId = uuid.v4();
            var user = {
                id: userId,
                name: userId,
            } as ChatUser;
            chatUsers.push(user);
        }
    }, 1500);
    return chatUsers;
}

export async function getMessages(chatGroupId: string): Promise<Array<ChatMessage>>{
    const chatMessages = [] as Array<ChatMessage>;
    setTimeout(() => {
        for (var i = 0; i < 25; i++) {
            var ChatMessage = {
                chatGroupId: chatGroupId,
                authorId: i % 2 === 0 ? myId : "other",
                content: `message-${i}`
            } as ChatMessage
            chatMessages.push(ChatMessage);
        }
    }, 1500);
    return chatMessages;
}