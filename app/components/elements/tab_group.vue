<template>
  <div>
    <TabHeader v-model="tab" :tabs="tabs" :class="headerClass" />
    <slot />
  </div>
</template>

<script>
  export default {
    name: "TabGroup",
    props: {
      headerClass: {
        type: String,
        required: false,
      },
    },
    data() {
      return {
        tab: 0,
        children: [],
      };
    },
    computed: {
      tabs() {
        return this.children.map((a, i) => ({ id: i, name: a.title }));
      },
      current() {
        return this.children[this.tab];
      },
    },
    mounted() {
      this.children = this.$children.filter(a => a.$options.name === "Tab");
    },
  };
</script>
