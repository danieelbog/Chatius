import { ChatMessage, ChatUser, ChatGroup } from './chat.service.dto';
var uuid = require("uuid");
const myId = uuid.v4();


export async function getUser(userId: string) {

}

export async function updateChatUser(chatUser: ChatUser) {

}

export async function deleteChatUser(chatUserId: string) {

}


export async function getChatGroupsFor(userId: string) {

}


export async function deleteChatGroupFor(userId: string, chatGroupId: string) {

}


export async function getChatMessagesFor(chatGroupdId: string) {

}

export async function addNewChatMessage(chatMessage: ChatMessage) {

}


export async function getUsers(userid: string): Promise<Array<ChatUser>>{
    const chatUsers = [] as Array<ChatUser>
    return await new Promise(resolve => setTimeout(()=> {
        for (var i = 0; i < 25; i++) {
            var userId = uuid.v4();
            var user = {
                id: userId,
                name: userId,
            } as ChatUser;
            chatUsers.push(user);
        }
        console.log("messages loaded")
        resolve(chatUsers)
    }, 0))
}

const chatMessages = [] as Array<ChatMessage>;
export async function getMessages(chatGroupId: string): Promise<Array<ChatMessage>>{
    return await new Promise(resolve => setTimeout(()=> {
        for (var i = 0; i < 225; i++) {
            var ChatMessage = {
                id:uuid.v4(),
                chatGroupId: chatGroupId,
                author: i % 2 === 0 ? { id: myId, name: myId } as ChatUser : { id: uuid.v4(), name: "User-" + i } as ChatUser,
                content: `message-${i}`
            } as ChatMessage
            chatMessages.push(ChatMessage);
        }
        resolve(chatMessages)
    }, 0))
}

const chatGroups = [] as Array<ChatGroup>;
export async function getChatGroups(ChatUserId: string): Promise<Array<ChatGroup>>{
return await new Promise(resolve => setTimeout(()=> {
    for (var i = 0; i < 25; i++) {
        var chatGroupd = {
            id: uuid.v4(),
            memebers: [
                {
                    id: myId,
                    name: myId
                } as ChatUser,
                {
                    id: uuid.v4(),
                    name: "UserName-"+ i
                } as ChatUser
            ],
            creator: {
                id: myId,
                name: myId
            } as ChatUser,
        } as ChatGroup
        chatGroups.push(chatGroupd);
        resolve(chatGroups);
    }
}, 1500))

}