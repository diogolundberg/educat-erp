<template>
  <div>
    <div class="flex items-center">
      <div class="iso circle bg-blue white bold center">{{ index }}</div>
      <div class="ml2 inline-block">
        <strong>{{ title }}</strong>
        <small class="block">{{ description }}</small>
      </div>
    </div>
    <div class="ml2 pl3 py2 border-gray border-left">
      <slot v-if="visible" />
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
      index: {
        type: Number,
        required: true,
      },
    },
    computed: {
      visible() {
        return this.$parent.value === this.index;
      },
    },
  };
</script>
