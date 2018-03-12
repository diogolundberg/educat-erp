<template>
  <div>
    <header class="fixed top-0 z3 w100 bg-white black shadow0">
      <div class="max-width-4 m-auto flex justify-between">
        <h1 class="m2 h3">CMMG</h1>
        <div class="m2">
          <a
            class="pl2"
            href="#"
            @click.prevent="$emit('notifications')">
            <img
              class="y2 pointer"
              src="../../assets/img/bell.svg">
          </a>
          <router-link
            class="pl2"
            to="/logout">
            <img
              class="y2 pointer"
              src="../../assets/img/logout.svg">
          </router-link>
        </div>
      </div>
    </header>
    <div class="p2 mb4" />
  </div>
</template>

<script>
  export default {
    name: "Header",
  };
</script>
