<template>
  <div>
    <header class="fixed top-0 left-0 z4 w100 bg-green-grad white">
      <div class="max-width-4 m-auto flex items-center justify-between p1">
        <div
          v-if="menu"
          class="m1 pointer sm-hide md-hide lg-hide"
          @click="$emit('sidebar')">
          <Icon
            :width="32"
            name="menu" />
        </div>
        <div class="flex-auto flex items-center xs-hide p1">
          <div>
            <img
              :style="{ height: '4rem' }"
              src="../../assets/img/logo.svg">
          </div>
        </div>

        <div class="p2">
          <Ball
            class="mr1"
            inline>
            <a
              :data-badge="notifications.length > 0 && notifications.length"
              class="pointer"
              href="javscript:void(0)"
              @click.prevent="showNotifications = !showNotifications">
              <Icon name="bell" />
            </a>
          </Ball>
          <Ball
            v-if="$store.getters.logged"
            inline>
            <router-link
              class="pointer"
              to="/logout">
              <Icon name="logout" />
            </router-link>
          </Ball>
        </div>
      </div>
    </header>
    <div class="m3 p2" />
    <Notifications
      v-if="showNotifications"
      :items="notifications"
      class="z3"
      @click="$emit('notification', $event)" />
  </div>
</template>

<script>
  export default {
    name: "Header",
    props: {
      menu: {
        type: Boolean,
        default: false,
      },
      notifications: {
        type: Array,
        default: () => [],
      },
    },
    data() {
      return {
        showNotifications: false,
      };
    },
  };
</script>
