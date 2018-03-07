<template>
  <div>
    <template v-if="$route.meta.nav">
      <Header @notifications="notifications = !notifications" />
      <Notifications
        v-if="notifications"
        @click="notifications = false" />
    </template>
    <router-view />
  </div>
</template>

<script>
  export default {
    name: "Application",
    data() {
      return {
        notifications: false,
      };
    },
  };
</script>
