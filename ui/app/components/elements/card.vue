<template>
  <div class="mb3 shadow1 rounded bg-white">
    <template v-if="title">
      <div class="px2 py1 bg-green-grad rounded-top yellow-dim relative">
        <div class="h3 m1">
          {{ title }}
          <Ball
            v-if="complete"
            class="bg-green shadow1 p6x ml1 inline-block">
            <Icon name="check" />
          </Ball>
          <Ball
            v-if="error"
            class="bg-red shadow1 p6x ml1 inline-block">
            <Icon name="error" />
          </Ball>
        </div>
      </div>
    </template>
    <div :class="{ p3: !noPadding }">
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
      noPadding: {
        type: Boolean,
        default: false,
        required: false,
      },
      error: {
        type: Boolean,
        default: false,
      },
    },
  };
</script>
