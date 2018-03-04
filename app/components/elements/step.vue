<template>
  <div>
    <div class="flex items-center">
      <Ball
        :index="index"
        class="mr2" />
      <div class="h4">
        {{ title }}
      </div>
    </div>
    <div class="sm-ml2 sm-pl3 py2 sm-border-left">
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
