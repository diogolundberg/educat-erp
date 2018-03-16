<template>
  <transition
    enter-class="right0"
    leave-to-class="right0">
    <aside
      class="fixed top-0 bottom-0 right-0 pt4 width-1 bg-white black shadow2 z2
        ease">
      <div
        v-for="item in items"
        :key="item.id"
        class="p2 border-bottom border-silver flex items-center">
          <Icon
            name="bell"
            black />
          <div class="px1">{{ item.title }}</div>
          <a
            href="#doc_01"
            class="btn btn-primary"
            @click="$emit('click')">
            OK
          </a>
      </div>
    </aside>
  </transition>
</template>

<script>
  export default {
    name: "Notifications",
    data() {
      return {
        items: [
          { id: "1", href: "#1", title: "Você precisa inserir seu RG!" },
        ],
      };
    },
  };
</script>
