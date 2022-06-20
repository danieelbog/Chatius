<template>
    <div class="about">
        <h1>This is an about page</h1>
        <div class="messages">
            <div v-for="message in messages">
                <div>
                    {{ message.username }} says: {{ message.message }}
                </div>
            </div>
        </div>
        <div class="input-zone">
            <label for="tbMessage">Message:</label>
            <input class="input-zone-input" type="text" v-model="message" />
            <button type="button" @click="send">Send</button>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent, ref } from '@vue/composition-api';
import * as signalR from "@microsoft/signalr";

export default defineComponent({
    setup() {
        const message = ref("");
        const username = new Date().getTime()
        const messages = ref([] as Array<{
            username: String,
            message: String
        }>);

        const connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7010/hub").build()
        connection.start().catch(err => document.write(err));
        connection.on("messageReceived", (username: string, message: string) => {
            messages.value.push({
                username: username,
                message: message
            })
        });

        function send() {
            connection.send("newMessage", username, message.value)
                .then(() => message.value = "");
        }

        return {
            send: send,
            message: message,
            messages: messages
        }
    }
});
</script>