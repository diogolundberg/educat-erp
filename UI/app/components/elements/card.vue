<template>
  <div class="mb3 shadow2 rounded bg-white">
    <template v-if="title">
      <div class="p3 bg-brown-grad rounded-top white relative">
        <h2 class="m0 thin">
          {{ title }}
          <Ball
            v-if="complete"
            class="bg-green shadow1 p6x ml1 inline-block">
            <Icon name="check" />
          </Ball>
          <Ball
            v-if="error"
            class="bg-green shadow1 p6x ml1 inline-block">
            <Icon name="error" />
          </Ball>
        </h2>
        <slot name="title" />
        <div
          v-if="closeable"
          class="absolute pointer top-0 right-0 px2 py0 h2"
          @click="$emit('close')">𐄂</div>
      </div>
    </template>
    <div class="p3">
      <slot />
    </div>
  </div>
</template>

<script>
  export default {
    name: "Card",
    props: {
      title: {
        type: String,
        required: false,
        default: null,
      },
      closeable: {
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
    },
  };
</script>
