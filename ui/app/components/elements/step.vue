<template>
  <div>
    <div
      v-if="!visible || disabled"
      :class="{ pointer: !disabled }"
      class="px2 py3 mb3 bg-white shadow1 rounded flex items-center"
      @click="$parent.goTo(index)">
      <Ball class="mr2">
        {{ index }}
      </Ball>
      <div class="border-right border-silver y3 mr2" />
      <div class="h5 line-height-3 flex-auto">
        <div>{{ title }}</div>
        <small class="dim">{{ description }}</small>
      </div>
      <Ball
        v-if="complete"
        class="bg-green shadow1 p6x">
        <Icon name="check" />
      </Ball>
      <Ball
        v-if="error"
        class="bg-red shadow1 p6x">
        <Icon name="error" />
      </Ball>
      <Ball
        v-if="warning"
        class="bg-yellow shadow1 p6x">
        <Icon name="error" />
      </Ball>
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
      complete: {
        type: Boolean,
        default: false,
      },
      error: {
        type: Boolean,
        default: false,
      },
      warning: {
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
      current() {
        return this.$parent.value;
      },
    },
  };
</script>
