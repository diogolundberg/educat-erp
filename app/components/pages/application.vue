<template>
  <div>
    <Header @notifications="notifications = !notifications" />
    <Notifications
      v-if="notifications"
      @click="notifications = false" />
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
