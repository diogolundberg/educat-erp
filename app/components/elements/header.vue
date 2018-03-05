<template>
  <div>
    <header class="fixed top-0 z3 w100 bg-white black shadow0">
      <div class="max-width-4 m-auto flex justify-between">
        <h1 class="m2 h3">CMMG</h1>
        <div class="flex flex-auto items-end">
          <router-link
            class="btn upcase input-line border-none ease"
            to="/">
            Matrícula
          </router-link>
          <router-link
            class="btn upcase input-line border-none ease"
            to="/list">
            Listagem
          </router-link>
        </div>
        <a
          href="#"
          data-badge="1"
          class="m2"
          @click="$emit('notifications')">
          <img
            class="y2 pointer"
            src="../../assets/img/bell.svg">
        </a>
      </div>
    </header>
    <div class="p2 mb4" />
  </div>
</template>

<script>
  export default {
    name: "Header",
    data() {
      return {
        tab: "",
        tabs: [
          { id: "1", }
        ],
      };
    },
  };
</script>
