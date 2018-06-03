<template>
  <transition
    enter-class="op0 right0-ch"
    leave-to-class="op0 right0-ch">
    <div
      v-if="value"
      class="fixed fill bg-dim ease flex z4"
      @click.self="$emit('input', false)">
      <div
        class="fixed top-0 bottom-0 right-0 width-2 bg-white shadow2 z2 ease
          flex flex-column">
        <div
          v-if="title"
          class="px3 py2 h2 bg-silver"
          v-text="title" />
        <div class="flex-auto overflow-auto">
          <slot />
        </div>
        <div
          v-if="$slots.footer"
          class="px3 py2 bg-silver right-align">
          <slot name="footer" />
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
  export default {
    name: "SideModal",
    props: {
      value: {
        type: Boolean,
        default: false,
      },
      title: {
        type: String,
        default: null,
      },
    },
  };
</script>
