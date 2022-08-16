<template>
    <div id="conversation-container" class="position-fixed bottom-0" :class="ConversationContainerDirection()">
        <chat-box-container v-if="!directionRight" :selectedChatGroup="selectedChatGroup"></chat-box-container>
        <div class="conversation-container-overlay shadow rounded-top" ref="chatContainer">
            <header class="bg-light rounded-top border" @click="toggleConversationContainer()">
                <div class="d-flex justify-content-between p-2">
                    <div class="d-flex align-items-center">
                        <div class="position-relative">
                            <img src="https://placekitten.com/g/30/30"
                                 alt=""
                                 width="32"
                                 height="32"
                                 class="d-inline-block align-text-top rounded-circle" />
                            <div class="status rounded-circle bg-success"></div>
                        </div>
                        <div class="ms-2">Messaging</div>
                    </div>
                    <div class="d-flex justify-content-between">
                        <button type="button" class="navbar-toggler border-0 shadow-none not-collapsed">
                            <span class="material-icons shadow-none">{{ expandedIcon }}</span>
                        </button>
                    </div>
                </div>
            </header>
            <div class="bg-light container-fluid border border-top-0 p-2">Search here</div>
            <section class="bg-light conversation-list border border-top-0">
                <div class="conversation-list-overlay">
                    <observable-infinite-scroll-wrapper @pageAtBottom="loadChatUsers()">
                        <template>
                            <div class="conversation-user p-2"
                                 @click="addSelectedChatGroup(chatUser)"
                                 v-for="chatUser in chatUsers"
                                 :key="chatUser.id">
                                {{ chatUser }}
                            </div>
                        </template>
                    </observable-infinite-scroll-wrapper>
                </div>
            </section>
        </div>
        <chat-box-container v-if="directionRight" :selectedChatGroup="selectedChatGroup"></chat-box-container>
    </div>
</template>

<script lang="ts">
    import { defineComponent, ref, watch, onMounted, onBeforeMount } from "@vue/composition-api";
    import ObservableInfiniteScrollWrapper from "../Layouts/wrappers/observable-infinite-scroll-wrapper.vue";
    import ChatBoxContainer from "./chat-box-container.vue";
    import "./conversation-container.scss";
    import { ChatGroup, ChatUser } from "./chat.service.dto";
    import { getUsers } from "./chat.service";
    import { connection } from "../../app/initializer/external/signalR";

    var uuid = require("uuid");
    const myId = uuid.v4();

    export default defineComponent({
        components: { ObservableInfiniteScrollWrapper, ChatBoxContainer },
        props: {
            directionRight: {
                type: Boolean,
                required: false,
                default: true,
            },
        },
        setup(props, context) {
            function ConversationContainerDirection(): string {
                if (props.directionRight) return "end-0";
                return "start-0";
            }

            onMounted(() => {
                broadcastUserConnection();
                setExpandedIcon();
                loadChatUsers();
               

            });

            connection.on("userConnected", (username: string) => {
                console.log("user connected:", username);
            });

            function broadcastUserConnection() {
                connection.send("userConnected", myId)
            }

            const expanded = ref(true);
            function toggleConversationContainer() {
                expanded.value = !expanded.value;
            }

            watch(
                () => expanded.value,
                (newVal: boolean, oldValue: boolean) => {
                    expanded.value = newVal;
                    setExpandedIcon();
                    setExpandedClass();
                },
            );

            const expandedIcon = ref("keyboard_arrow_down");
            function setExpandedIcon() {
                if (expanded.value) expandedIcon.value = "keyboard_arrow_down";
                else expandedIcon.value = "keyboard_arrow_up";
            }

            const chatContainer = ref(null as unknown as HTMLElement);
            function setExpandedClass() {
                if (!chatContainer.value) return;

                if (expanded.value) {
                    chatContainer.value.classList.remove("conversation-container-is-minimized");
                    chatContainer.value.classList.add("conversation-container-is-expanded");
                } else {
                    chatContainer.value.classList.remove("conversation-container-is-expanded");
                    chatContainer.value.classList.add("conversation-container-is-minimized");
                }
            }

            const chatUsers = ref([] as Array<ChatUser>);
            async function loadChatUsers() {
                chatUsers.value = await getUsers(myId);
            }

            const selectedChatGroup = ref(null as unknown as ChatGroup);
            function addSelectedChatGroup(chatUser: ChatUser) {
                var chatGroup = {
                    id: uuid.v4(),
                    memebers: [myId, chatUser.id],
                    creatorId: myId,
                } as ChatGroup;

                selectedChatGroup.value = chatGroup;
            }

            return {
                chatContainer: chatContainer,
                ConversationContainerDirection: ConversationContainerDirection,
                toggleConversationContainer: toggleConversationContainer,
                expandedIcon: expandedIcon,
                loadChatUsers: loadChatUsers,
                chatUsers: chatUsers,
                addSelectedChatGroup: addSelectedChatGroup,
                selectedChatGroup: selectedChatGroup,
            };
        },
    });
</script>
