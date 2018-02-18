<template>
  <div class="shadow1 rounded bg-white">
    <h3 class="p2 m0 rounded-top bg-blue white" v-if="title">
      {{ title }}
    </h3>
    <div class="p2">
      <slot />
    </div>
    <Btn class="btn-primary rounded-bottom block center"
      :label="action" v-if="action" @click="$emit('click')" />
  </div>
</template>

<script>
  export default {
    name: "Card",
    props: {
      title: {
        type: String,
        required: false,
      },
      action: {
        type: String,
        required: false,
      },
    },
  };
</script>
