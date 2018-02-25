<template>
  <div>
    <div class="flex items-baseline">
      <div class="mr2 x2 y2 p4x circle bg-blue bold white h4 center shadow1">
        {{ index }}
      </div>
      <div class="h4 bold">
        {{ title }}
      </div>
    </div>
    <div class="ml2 pl3 py2 border-gray border-left">
      <slot v-if="visible" />
      <div v-if="!visible && description">{{ description }}</div>
    </div>
  </div>
</template>

<script>
  export default {
    name: "Step",
    props: {
      title: {
        type: String,
        required: true,
      },
      description: {
        type: String,
        required: true,
      },
    },
    computed: {
      visible() {
        return this.$parent.value === this.index;
      },
      index() {
        return this.$parent.$slots.default.filter(s => s.tag)
          .indexOf(this.$vnode) + 1;
      },
    },
  };
</script>
