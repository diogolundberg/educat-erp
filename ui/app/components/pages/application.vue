<template>
  <div>
    <Header
      v-if="$route.meta.header"
      menu
      @sidebar="sidebar = !sidebar" />
    <div class="flex">
      <Sidebar
        v-if="$route.meta.header"
        :visible="sidebar"
        :links="links"
        class="shrink0"
        @exit="sidebar = false" />
      <router-view
        class="flex-auto px2" />
    </div>
  </div>
</template>

<script>
  export default {
    name: "Application",
    data() {
      return {
        sidebar: false,
      };
    },
    computed: {
      links() {
        return [
          { url: "/approvals", name: "Pendencias" },
          { url: "/onboardings", name: "Onboardings" },
        ];
      },
    },
  };
</script>
