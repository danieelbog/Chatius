<template>
  <div class="container-fluid">
    <div class="row flex-xl-nowrap2">
      <main-navbar></main-navbar>
      <div class="container-fluid mt-3 mb-3">
        <div class="row flex-xl-nowrap">
          <left-sidebar></left-sidebar>
          <main-content></main-content>
          <right-sidebar></right-sidebar>
        </div>
      </div>
      <main-footer></main-footer>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import MainNavbar from "./navbars/main-navbar.vue";
import MainContent from "./content/main-content.vue";
import MainFooter from "./footers/main-footer.vue";
import LeftSidebar from "./sidebars/left-sidebar.vue";
import RightSidebar from "./sidebars/right-sidebar.vue";

export default defineComponent({
  components: {
    "main-navbar": MainNavbar,
    "main-content": MainContent,
    "main-footer": MainFooter,
    "left-sidebar": LeftSidebar,
    "right-sidebar": RightSidebar,
  },
});
</script>
