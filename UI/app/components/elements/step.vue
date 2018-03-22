<template>
  <div>
    <div
      v-if="!visible || disabled"
      class="p2 mb3 bg-white shadow2 rounded flex items-center"
      :class="{ pointer: !disabled }"
      @click="$parent.goTo(index)">
      <Ball class="mr2">
        {{ index }}
      </Ball>
      <div class="h5 line-height-3 flex-auto">
        <div>{{ title }}</div>
        <small class="dim">{{ description }}</small>
      </div>
      <div v-if="complete">
        <div class="x1 y1 circle relative pointer x2 y2 p6x shadow1 bg-green">
          <Icon name="check" />
        </div>
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
      complete: {
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
