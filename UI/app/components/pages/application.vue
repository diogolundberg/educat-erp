<template>
  <div>
    <Header
      v-if="$route.meta.header"
      menu
      @sidebar="sidebar = !sidebar" />
    <Sidebar
      v-if="sidebar"
      @exit="sidebar = false"
      :links="links" />
    <router-view />
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
