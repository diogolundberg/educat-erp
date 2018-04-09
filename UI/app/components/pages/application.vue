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
          { url: "/enrollments/academic", name: "Pendências Acadêmicas" },
          { url: "/enrollments/finance", name: "Pendências Financeiras" },
        ];
      },
    },
  };
</script>
