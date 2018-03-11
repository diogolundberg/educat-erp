<template>
  <div>
    <div
      v-if="!visible || disabled"
      class="p2 mb3 bg-white shadow2 rounded flex items-center"
      :class="{ pointer: $parent.value > index }"
      @click="$parent.goTo(index)">
      <Ball
        :index="index"
        class="mr2" />
      <div class="h5 line-height-3">
        <div>{{ title }}</div>
        <small class="dim">{{ description }}</small>
      </div>
    </div>
    <div
      v-if="visible && !disabled"
      class="py2">
      <slot />
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
      disabled: {
        type: Boolean,
        default: false,
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
