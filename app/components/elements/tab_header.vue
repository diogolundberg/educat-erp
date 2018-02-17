<template>
  <div class="flex border-bottom pl3">
    <span v-for="tab in tabs" :key="tab.id">
      <a @click="$emit('input', tab.id)"
        class="btn upcase h-inset-bottom border-none"
        :class="{ 'inset-bottom': value === tab.id }">
        {{ tab.name }}
      </a>
    </span>
  </div>
</template>

<script>
  export default {
    name: "TabHeader",
    props: {
      value: {
        type: [Number, String],
        default: null,
      },
      tabs: {
        type: Array,
        default: () => [],
      },
    },
  };
</script>
