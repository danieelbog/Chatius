import { ChatMessage, ApplicationUser, ChatGroup } from './chat.service.dto';
var uuid = require("uuid");
const myId = uuid.v4();

export async function getUsers(userid: string): Promise<Array<ApplicationUser>>{
    const chatUsers = [] as Array<ApplicationUser>
    return await new Promise(resolve => setTimeout(()=> {
        for (var i = 0; i < 25; i++) {
            var userId = uuid.v4();
            var user = {
                id: userId,
            } as ApplicationUser;
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
                author: i % 2 === 0 ? { id: myId } as ApplicationUser : { id: uuid.v4() } as ApplicationUser,
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
                } as ApplicationUser,
                {
                    id: uuid.v4(),
                } as ApplicationUser
            ],
            creator: {
                id: myId,
            } as ApplicationUser,
        } as ChatGroup
        chatGroups.push(chatGroupd);
        resolve(chatGroups);
    }
}, 1500))

}